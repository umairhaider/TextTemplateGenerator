using TemplateGeneratorTests.DataModel;
using TextTemplateGenerator;
namespace TemplateGeneratorTests;

public class GenerateTextMethodTest
{
    [Fact]
    public void GenerateText_ReplacesPlaceholders()
    {
        /*** Arrangement ***/
        var addressDataModel = new AddressDataModel
        {
            Name = "John Doe",
            Address = new Address
            {
                City = "Budapest",
                Line1 = "Main Street, 1"
            }
        };

        var templateGenerator = new TemplateGenerator();
        string template = "Hello {Name},\nWe will be glad to see you in our office in {Address.City} at {Address.Line1}.\nLooking forward to meeting with you!\nBest, Our company.";

        /*** Action ***/
        string result = templateGenerator.GenerateText(template, addressDataModel);

        /*** Assertion ****/
        string expected = "Hello John Doe,\nWe will be glad to see you in our office in Budapest at Main Street, 1.\nLooking forward to meeting with you!\nBest, Our company.";
        Assert.Equal(expected, result);
    }
}
