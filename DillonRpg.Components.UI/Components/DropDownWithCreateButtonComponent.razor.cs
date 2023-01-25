
namespace DillonRPG.Components.UI.Components;

public partial class DropDownWithCreateButtonComponent<TValue>
{
    [Parameter]
    public EventCallback ButtonClick { get; set; }

    [Parameter]
    public string LabelText { get; set; } = typeof(TValue).ToString();

    [Parameter]
    public IEnumerable<object> Data { get; set; } = Enumerable.Empty<object>();

    [Parameter]
    public TValue? BindValue { get; set; }

    [Parameter]
    public string? TextProperty { get; set; }

    [Parameter]
    public string? ValueProperty { get; set; }
}
