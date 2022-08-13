// /////////////////////////////////////////////////////////////////////////////
// PLEASE DO NOT RENAME OR REMOVE ANY OF THE CODE BELOW. 
// YOU CAN ADD YOUR CODE TO THIS FILE TO EXTEND THE FEATURES TO USE THEM IN YOUR WORK.
// /////////////////////////////////////////////////////////////////////////////
namespace PlayerWebApi.Data.Entities;

public class PlayerSkill
{
    public int Id { get; set; }
    public string Skill { get; set; }
    public int Value { get; set; }
    public int PlayerId { get; set; }
}
