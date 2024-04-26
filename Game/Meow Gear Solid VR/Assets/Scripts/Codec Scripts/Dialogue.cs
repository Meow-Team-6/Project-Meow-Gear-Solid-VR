using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string callerName = "Professor Meow";

    // Text uses 3 to 10 lines
    /*[TextArea(3, 10)]
    public string[] sentences;*/

    // Default dialogue dictionary
    // Updates after certain points in game is reached
    public Dictionary<int, List<string>> defDialogue = new Dictionary<int, List<string>>()
    {
        //First event is a mission reminder.
        { 0, new List<string> {"Go on, what are you doing!", "Head towards the elevator and begin your mission."} },
        //Second Event is after you've found your gun
        { 1, new List<string> {"Glad you called! (I was getting a little lonely...)", "Ah!", "That's right, your mission.",
        "Head further into the base, there should be a Hangar where the dogs are keeping 'Meow Gear.'", "Make sure you keep an eye out for any gaurds patroling the area!",
        "Thankfully you have your handy dandy wrist mounted radar!", "Give it a look before you enter a room, you never know what's hiding in the back.",
        "Oh and by the way Paws, as you know Weapons and Equipment will be OSP (on site procurement).", "This is a weapons warehouse though.", "So I'm sure you'll be able to find whatever you need!"} },
        { 2, new List<string> {"Keep an eye out!", "As you head further into the base, the more enemies there are.", "Keep you eyes on your map,","and try to stay out of trouble!"} },
        { 3, new List<string> {"Go on, what are you doing!", "Head towards the door and stop Meow Gear!"} }

    };
    
    // Event dialogue dictionary
    public Dictionary<string, List<string>> eventDialogue = new Dictionary<string, List<string>>()
    {
        //Plays when the player enters the elevator for the first time
        {"Event01", new List<string> {"Come in Agent Paws, this is the Professor.", "I see you've already infiltrated the Dog's base.",
        "Age hasn't slowed you down one bit!", "I'll be keeping in touch with you using the Codec system.", "If you have any questions, give my number a call using the 'Call' Button.",
        "Oh also that reminds me.","The 'Call' button is bound to the 'B' button on your handy dandy Oculus Touch controller!","Anyways, I'll leave you be for now. Goodluck out there!"} },

        {"Event02", new List<string> {"Hey, great work out there!", "Oh by the way, have you run into any weapons yet?", "I'm sure by now there's got to have been at least one basic pistol.",
        "That reminds me!", "If you see any object that's spinning, that means you can pick it up and add it to your inventory!","Oh and you can't see it, but to your right is your holster.","Put any item you get tired of holding in it!", "Now get out there and find some pickups!", "See if there's any spinning guns or med kits nearby!"} },

        {"Event03", new List<string> {"Great work out there Paws!","Just beyond that door is Meow Gear.", "You got this!"} },

        {"CodecTrigger1", new List<string> {"Why are you running."} }
    };

    public Dictionary<int, List<string>> finalDialogue = new Dictionary<int, List<string>>()
    {
        //First event is a mission reminder.
        { 0, new List<string> {"Ah yes...", "Hello... Brother...", "It would appear you finally made it to me and...", "the nuclear weapon, MEOW GEAR!",
        "I'm glad you've already done me the favor of removing your gear.", "Now we can have a battle to decide who is the superior Cat.",
        "I wont lose so easily, you better give it your all!"} }
    };
}
