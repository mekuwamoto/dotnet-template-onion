using Onion.Template.Application.Commom.Enums;

namespace Onion.Template.Application.Commom.Attributes;

public class InjectionAttribute : Attribute
{
    public DI Di;
    public InjectionAttribute(DI di) => Di = di;
}