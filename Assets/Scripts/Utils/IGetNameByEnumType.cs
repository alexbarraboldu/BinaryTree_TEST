using System;

public interface IGetNameByEnumType<E> where E : Enum
{
	string GetNameByEnumType(E type);
}
