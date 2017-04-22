using System;
using Prototype.GameInformation;
using Prototype.GameSystem;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {

            GameManager gameManager = new GameManager(new TestPlayer("a"), new TestPlayer("b"), 200);

            ProtoTypeUI protoType = new ProtoTypeUI(gameManager);
        }
    }
}