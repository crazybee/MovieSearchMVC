$(document).ready(function () {
    $('thead > tr > th > a[href*="sort=@grid.SortColumn"]').parent().append("@(grid.SortDirection == SortDirection.Ascending ? "▲" : " ▼")");
 
    if ($.cookie('searchPanelVis') == null) {
        $.cookie('searchPanelVis', 'hidden'); // initial load
        $('#searchParameters').hide();
    }
    else {
        var toggleStatus = $.cookie('searchPanelVis'); // restore hide/show
        if (toggleStatus == 'hidden') {
            $('#searchParameters').hide();
        }
        else {
            $('#searchParameters').show();
        }
    }
 
    // toggle the search panel on/off
    $('#searchPanelLink').click(function () {
        $('#searchParameters').slideToggle('slow', function(){
            if ($('#searchParameters').is(':hidden')) {
                $.cookie('searchPanelVis', 'hidden');
            }
            else {
                $.cookie('searchPanelVis', 'visible');
            }
        })
    });
 
    // hover effect for the search panel icon
    $("#searchPanelLink").mouseenter(function () {
        $("#searchPanelLink").attr('src', '../Content/images/search_hover.png');
    });
    $("#searchPanelLink").mouseleave(function () {
        $("#searchPanelLink").attr('src', '../Content/images/search.png');
    });
    // clear search parameters and reload the grid with all records
    $('#searchReset').click(function () {
        $('#movieName').val('');
    });
 
    $(':text').focus(function () {
        var nameValue = $(this).attr('name');
        $.cookie('lastFocus', nameValue);
    });
    if ($.cookie('lastFocus') != null) {
        var focused = $.cookie('lastFocus');
        switch (focused) {
            case 'zipfilter':
                $('#zipfilter').select().focus();
                break;
          
            default:
                break;
        }
    }
});