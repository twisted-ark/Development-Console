using System.Collections.Generic;

namespace TwistedArk.Development.Console
{
    public sealed class ConsoleTab : IGuiElementGroup
    {
        private Dictionary<string, GuiElementBase> namedElements = new Dictionary<string, GuiElementBase> ();

        private List<GuiElementBase> elements;

        public string Name { get; private set; }

        public int ElementCount => elements.Count;

        public ConsoleTab (string name, params GuiElementBase[] elements)
        {
            Name = name;
            this.elements = new List<GuiElementBase> (elements);
        }

        public GuiElementBase GetGuiElement (int index)
        {
            return elements[index];
        }

        public void Add (GuiElementBase element)
        {
            elements.Add (element);
            namedElements.Add (element.Label, element);
        }

        public void Remove (GuiElementBase elementBase)
        {
            elements.Remove (elementBase);
            namedElements.Remove (elementBase.Label);
        }
        
        public void Remove (string elementName)
        {
            if (TryGetNamedElement (elementName, out GuiElementBase elementBase))
            {
                elements.Remove (elementBase);
                namedElements.Remove (elementBase.Label);
            }
        }

        public bool TryGetNamedElement<T> (string name, out T element) where T : GuiElementBase
        {
            element = null;
            return namedElements.TryGetValue (name, out var temp) && (element = temp as T) != null;
        }
        
    }

}