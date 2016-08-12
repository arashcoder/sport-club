$(document).ready(function () {

    var fixHelperModified = function (e, tr) {
        var $originals = tr.children();
        var $helper = tr.clone();
        $helper.children().each(function (index) {
            $(this).width($originals.eq(index).width())
        });
        return $helper;
    },
    updateIndex = function (e, ui) {
        var ids = [];
        var orders = [];
        $('td.index', ui.item.parent()).each(function (i) {
            $(this).html(i + 1);
            ids.push($(this).data('id'));
            orders.push(i + 1);
        });


        $.ajax({
            type: "POST",
            url: $(this).data('order-url'),
            data: { clubId : $('#clubId').val(), idList: ids, orderList : orders },
            cache: false
        });
    };

    $("#pic-table tbody").sortable({
        helper: fixHelperModified,
        stop: updateIndex
    }).disableSelection();

    $('.delete').on('click', function (pos, item) {
      //  var clubId = $('#clubId').val();
        var pictureId = $(this).data('id');
        var tr = $(this).parent().parent();// gets the current tr. Used for removing the row after successful deletion.
        $.ajax({
            type: "POST",
            url: $(this).data('delete-url'),
            data: { picId: pictureId },
            cache: false,
            success: function (data) {
                tr.remove();
            }
        });
    });

    $('.activation').on('click', function (pos, item) {
        //  var clubId = $('#clubId').val();
        var pictureId = $(this).data('id');
        var current = $(this);
        $.ajax({
            type: "POST",
            url: $(this).data('activation-url'),
            data: { id: pictureId },
            cache: false,
            success: function (data) {
                if (data === 'true') {
                    current.removeClass('glyphicon-asterisk');
                    current.addClass('glyphicon-arrow-down');
                } else {
                    current.removeClass('glyphicon-arrow-down');
                    current.addClass('glyphicon-asterisk');
                }
               
            }
        });
    });

    $('.default').on('click', function (pos, item) {
         var clubId = $('#clubId').val();
        var pictureId = $(this).data('id');
        var current = $(this);
        $.ajax({
            type: "POST",
            url: $(this).data('default-url'),
            data: { clubId : clubId, picId: pictureId },
            cache: false,
            success: function (data) {
                $('.glyphicon-bell').each(function(index, item) {
                    $(item).removeClass('glyphicon-bell');
                    $(item).addClass('glyphicon-tree-deciduous');
                });
                current.removeClass('glyphicon-tree-deciduous');
                current.addClass('glyphicon-bell');
            }
        });
    });
});