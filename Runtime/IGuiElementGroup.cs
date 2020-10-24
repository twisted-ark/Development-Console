namespace TwistedArk.Development.Console
{
    public interface IGuiElementGroup
    {
        void Add (GuiElementBase element);

        void Remove (GuiElementBase element);

        void Remove (string elementName);
        
        bool TryGetNamedElement<T> (string name, out T element) where T : GuiElementBase;
    }

}