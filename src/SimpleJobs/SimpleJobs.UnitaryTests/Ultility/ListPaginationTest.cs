namespace SimpleJobs.UnitaryTests.Ultility;

public class ListPaginationTests : BaseTest
{
    [Fact]
    public void PageCount_CorrectlyCalculatesNumberOfPages()
    {
        IListPagination<int> pagination = new ListPagination<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3);
        int pageCount = pagination.PageCount;

        pageCount.Should().Be(4);
    }

    [Fact]
    public void GetPage_ReturnsCorrectPage()
    {
        IListPagination<int> pagination = new ListPagination<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3);
        var page2 = pagination.GetPage(1);

        page2.Should().Contain(new List<int> { 4, 5, 6 });
    }

    [Fact]
    public void Count_ReturnsCorrect()
    {
        List<int> values = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        IListPagination<int> pagination = new ListPagination<int>(values, 3);
        int total = pagination.Count;

        total.Should().Be(values.Count);
    }

    [Fact]
    public void FirstPage_ReturnsFirstPage()
    {
        IListPagination<int> pagination = new ListPagination<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3);
        var firstPage = pagination.FirstPage();

        firstPage.Should().Contain(new List<int> { 1, 2, 3 });
    }

    [Fact]
    public void LastPage_ReturnsLastPage()
    {
        var pagination = new ListPagination<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3);
        var lastPage = pagination.LastPage();

        lastPage.Should().Contain(new List<int> { 10 });
    }
}
