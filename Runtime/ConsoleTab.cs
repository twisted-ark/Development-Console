using System.Collections.Generic;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class ConsoleTab
    {
        private List<GuiElement> elements;

        public string Name { get; private set; }

        public int ElementCount => elements.Count;

        public ConsoleTab (params GuiElement[] elements) : this (string.Empty, elements)
        {
        }
        
        public ConsoleTab (string name, params GuiElement[] elements)
        {
            Name = name;
            this.elements = new List<GuiElement> (elements);
        }

        public GuiElement GetGuiElement (int index)
        {
            return elements[index];
        }

        public ConsoleTab Add (GuiElement element)
        {
            elements.Add (element);
            return this;
        }

        public ConsoleTab AddRange (IEnumerable<GuiElement> elements)
        {
            this.elements.AddRange (elements);
            return this;
        }
    }

}