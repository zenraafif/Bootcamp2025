using System;
using System.Collections;

Stack undoStack = new Stack();
Stack redoStack = new Stack();

undoStack.Push("A");
undoStack.Push("B");
undoStack.Push("C");
undoStack.Push("D");
undoStack.Push("E");

Console.WriteLine("Isi Undo Stack:");
foreach (var item in undoStack)
{
    Console.WriteLine(item);
}

Console.WriteLine("\nUndo satu aksi:");
if (undoStack.Count > 0)
{
    var undoAction = undoStack.Pop();
    redoStack.Push(undoAction);
    Console.WriteLine("Undo: " + undoAction);
}

Console.WriteLine("\nIsi Undo Stack setelah Undo:");
foreach (var item in undoStack)
{
    Console.WriteLine(item);
}

Console.WriteLine("\nRedo satu aksi:");
if (redoStack.Count > 0)
{
    var redoAction = redoStack.Pop();
    undoStack.Push(redoAction);
    Console.WriteLine("Redo: " + redoAction);
}

Console.WriteLine("\nIsi Undo Stack setelah Redo:");
foreach (var item in undoStack)
{
    Console.WriteLine(item);
}