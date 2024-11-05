using System.Globalization;
using System.Reflection;
using Xunit.Sdk;

namespace _3DBook.UnitTests.Core.Helpers;

public class UseCultureAttribute : BeforeAfterTestAttribute
{
    private readonly CultureInfo _culture;
    private CultureInfo _originalCulture;
    private CultureInfo _originalUICulture;

    public UseCultureAttribute(string cultureName)
    {
        _culture = new CultureInfo(cultureName);
    }

    public override void Before(MethodInfo methodUnderTest)
    {
        _originalCulture = CultureInfo.CurrentCulture;
        _originalUICulture = CultureInfo.CurrentUICulture;

        CultureInfo.CurrentCulture = _culture;
        CultureInfo.CurrentUICulture = _culture;
    }

    public override void After(MethodInfo methodUnderTest)
    {
        CultureInfo.CurrentCulture = _originalCulture;
        CultureInfo.CurrentUICulture = _originalUICulture;
    }
}