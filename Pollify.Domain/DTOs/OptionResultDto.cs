namespace Pollify.Domain.DTOs;

public class OptionResultDto
{
    public string Question { get; set; }
    public int optionText { get; set; }
    public int OptionCount { get; set; }
    public decimal OptionPercent { get; set; }
}