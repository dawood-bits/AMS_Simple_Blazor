namespace AMSWeb.DTOs;
public class UserDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Employee";
    public string? Department { get; set; }
    public string? RFIDCode { get; set; }
    public string? PhotoPath { get; set; }
}
