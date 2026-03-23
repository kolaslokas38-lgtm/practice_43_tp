namespace Task;

public interface IPill
{
    int DosageMg { get; set; }
    void Take();
}