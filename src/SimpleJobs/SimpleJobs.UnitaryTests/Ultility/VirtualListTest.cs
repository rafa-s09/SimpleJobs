namespace SimpleJobs.UnitaryTests.Ultility;

public class VirtualListTest
{
    [Fact]
    public void PaginacaoFuncionaCorretamente()
    {
        var dados = new List<string> { "A", "B", "C", "D", "E" };
        var listaVirtual = new VirtualList<string>(dados, 2);

        listaVirtual.GoToPage(2);
        Assert.Equal(["C", "D"], listaVirtual.PagedList);
    }

    [Fact]
    public void FiltragemFuncionaCorretamente()
    {
        var dados = new List<string> { "Apple", "Banana", "Cherry", "Date" };
        var listaVirtual = new VirtualList<string>(dados, 3);

        listaVirtual.Filter(item => item.StartsWith('B'));
        Assert.Equal(["Banana"], listaVirtual.PagedList);
    }

    [Fact]
    public void ObtemQuantidadeItensCorreta()
    {
        var dados = new List<string> { "A", "B", "C", "D" };
        var listaVirtual = new VirtualList<string>(dados, 2);

        Assert.Equal(4, listaVirtual.ItensCount);
    }

    [Fact]
    public void ObtemNumeroTotalPaginasCorreto()
    {
        var dados = new List<string> { "A", "B", "C", "D", "E", "F" };
        var listaVirtual = new VirtualList<string>(dados, 2);

        Assert.Equal(3, listaVirtual.PageCount);
    }

    [Fact]
    public void ObtemIndicePaginaAtualCorreto()
    {
        var dados = new List<string> { "A", "B", "C", "D", "E" };
        var listaVirtual = new VirtualList<string>(dados, 2);

        Assert.Equal(1, listaVirtual.PageIndex);
    }

    [Fact]
    public void ObtemIndiceItemEspecificoCorreto()
    {
        var dados = new List<string> { "A", "B", "C", "D", "E" };
        var listaVirtual = new VirtualList<string>(dados, 2);

        Assert.Equal(2, listaVirtual.GetItemIndex("C"));
    }
}
