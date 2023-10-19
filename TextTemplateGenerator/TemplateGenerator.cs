using System.Reflection;

namespace TextTemplateGenerator;
public class TemplateGenerator
{
    /// <summary>
    /// Generates text by replacing placeholders in the provided template with values from the data model.
    /// </summary>
    /// <typeparam name="T">The type of the data model.</typeparam>
    /// <param name="templateStr">The template string containing placeholders to be replaced.</param>
    /// <param name="dataModel">The data model used to replace placeholders in the template.</param>
    /// <returns>The generated text with placeholders replaced by corresponding values from the data model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the data model is null.</exception>
    public string GenerateText<T>(string templateStr, T dataModel) where T : class
    {
        if (dataModel == null)
        {
            throw new ArgumentNullException(nameof(dataModel));
        }

        string resultText = templateStr;
        resultText = ReplacePlaceholdersRecursively(dataModel, resultText);

        return resultText;
    }

    /// <summary>
    /// Recursively replaces placeholders in the given text with values from the data model.
    /// </summary>
    /// <param name="data">The data model used to replace placeholders in the text.</param>
    /// <param name="text">The text containing placeholders to be replaced.</param>
    /// <param name="nestedProperty">The nested property used to build the placeholder for nested values (optional).</param>
    /// <returns>The text with placeholders replaced by corresponding values from the data model.</returns>
    private string ReplacePlaceholdersRecursively(object data, string text, string nestedProperty = "")
    {
        string result = text;

        foreach (PropertyInfo property in data.GetType().GetProperties())
        {
            string placeholder = nestedProperty == "" ? $"{{{property.Name}}}" : $"{{{nestedProperty}.{property.Name}}}";
            string value = property.GetValue(data)?.ToString() ?? string.Empty;
            result = result.Replace(placeholder, value);

            if (value != null)
            {
                string nestedPlaceholder = $"{{{property.Name}.";
                if (result.Contains(nestedPlaceholder))
                {
                    result = ReplacePlaceholdersRecursively(property.GetValue(data) ?? string.Empty, result, property.Name);
                }
            }
        }

        return result;
    }

}
