
namespace DillonRPG.Components.UI.Components;

public partial class DropDownWithCreateButtonComponent
{
    [Parameter]
    public EventCallback ButtonClick { get; set; }

    [Parameter]
    public EventCallback<object> OnChange { get; set; }

    [Parameter]
    public string LabelText { get; set; } = string.Empty;

    [Parameter]
    public IEnumerable<object> Data { get; set; } = Enumerable.Empty<object>();

}
