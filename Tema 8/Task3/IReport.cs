namespace Task;

public interface IReport<T>
{
    string Generate(T data);
}