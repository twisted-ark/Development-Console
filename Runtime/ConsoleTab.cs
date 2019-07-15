using System.Collections.Generic;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class ConsoleTab
    {
        private List<GuiElementBase> elements;

        public string Name { get; private set; }

        public int ElementCount => elements.Count;

        public ConsoleTab (params GuiElementBase[] elementsBase) : this (string.Empty, elementsBase)
        {
        }
        
        public ConsoleTab (string name, params GuiElementBase[] elementsBase)
        {
            Name = name;
            this.elements = new List<GuiElementBase> (elementsBase);
        }

        public GuiElementBase GetGuiElement (int index)
        {
            return elements[index];
        }

        public ConsoleTab Add (GuiElementBase elementBase)
        {
            elements.Add (elementBase);
            return this;
        }

        public ConsoleTab AddRange (IEnumerable<GuiElementBase> elements)
        {
            this.elements.AddRange (elements);
            return this;
        }
    }

}