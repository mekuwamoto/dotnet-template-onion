using Onion.Template.Application.Commom.Enums;

namespace Onion.Template.Application.Commom.Attributes;

public class DependencyAttribute : Attribute
{
    public DI Di;
    public DependencyAttribute(DI di) => Di = di;
}