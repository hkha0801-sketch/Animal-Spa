# WPF reusable components

This refactor changes component structure, not the screen design or backend behavior.

## Components and consumers

| Component / resource | Consumers | WPF mechanism |
| --- | --- | --- |
| `Components/Shared/PhotoUpload` | AddCustomerView, AddPetView | UserControl, DependencyProperty, two-way PhotoPath binding, caller-provided fallback content |
| `Components/Shared/ServiceCard` | ServiceView in both Customer and Receptionist dashboards | UserControl, DependencyProperty, bubbling Chosen event |
| Service list | ServiceView | ItemsControl, DataTemplate, ObservableCollection, INotifyPropertyChanged |
| `ComponentStyles.xaml` | ServiceCard and both add forms | ResourceDictionary, BasedOn styles, ControlTemplate, hover/pressed triggers |
| `ComponentAppearance.CornerRadius` | Shared action-button template | Attached property; retains different corner radii per form |

The controls keep their consumer's DataContext. Internal bindings use ElementName=Root, so consumers can bind component properties to their own data.

## PhotoUpload

The component owns the file dialog, image decoding without locking the source file, default upload artwork, preview, and reset. The form owns its input fields and status message.

```xml
<shared:PhotoUpload PhotoPath="{Binding PhotoPath}"
                    Subject="pet" Width="445" Height="455"
                    PhotoFill="White" BorderBrush="#6F6F6F"
                    StrokeDashArray="7 5"/>
```

`FallbackContent` allows each form to retain its existing fallback artwork. `StatusChanged` reports selection and decoding errors. `ResetPhoto()` restores the default image. Save remains a UI placeholder, as before.

## ServiceCard and service list

One card template replaces three duplicated visual trees. ServiceOption supplies name, description, duration, selection state, title size and spacing. ServiceView retains the existing category data and forwards the same ServiceSelected event to the dashboards.

The first two cards retain 35-point titles, the third retains a 33-point title; cards remain 550 × 375 with 30-unit gaps. Selection, hover and pressed colors/opacity are unchanged. All four category mappings and the fallback category remain unchanged.

## Preserved appearance

- Existing Viewbox, Canvas positions, backgrounds, images, labels and input controls.
- Original photo-area dimensions, outline, dash pattern and fallback artwork.
- Customer action buttons: height 58, font 25, radius 16.
- Pet action buttons: height 60, font 26, radius 14.
- Original service-card visual tree and button template.

## Validation

Static checks compare the extracted card visual tree against each original card, resolve its bindings to the original sample values, compare photo fallback trees/coordinates/dimensions, check surviving named form controls, parse every XAML file, and run git diff --check.

Release compilation passed with .NET SDK 8.0.426 and Windows targeting on the Linux build host (0 errors reported). Static checks above also passed. WPF cannot run on this host, so runtime interaction and pixel comparison on Windows are still pending. On Windows, verify with:

```powershell
dotnet restore
dotnet build Animal-Spa.csproj -c Release
dotnet run
```

Manual acceptance:

1. Compare both add forms against main at the same window size, including fallback artwork in the Designer.
2. Select, replace and cancel photo selection; reset each form and check status text and default image.
3. Test an invalid image file; the previous preview should remain visible with an error message.
4. Open every service category; compare all three cards, including the third title's font size.
5. Select different cards; only the last selected card should be highlighted.
6. Check the existing flow into service detail and booking for Receptionist, and service detail for Customer.
7. Resize the window and confirm the existing Viewbox scaling behavior.

## Reporting points

Demonstrate one photo component reused by two forms and one service-card component repeated from data. Explain DependencyProperty for binding, DataTemplate for rendering collections, routed events for communication, and ResourceDictionary/ControlTemplate for shared appearance. Database persistence and authentication remain outside this refactor.
