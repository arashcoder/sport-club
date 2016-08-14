$(document).ready(function () {

    var fixHelperModified = function (e, tr) {
        var $originals = tr.children();
        var $helper = tr.clone();
        $helper.children().each(function (index) {
            $(this).width($originals.eq(index).width());// updates displayorder column's text to reflect the new display order of the images.
        });
        return $helper;
    },
    updateIndex = function (e, ui) {
        /**
         * Called when user drops the table row. It creates two arrays: one containing the id of the rows, and
         * one for the display order. It then sends these via ajax to the server.
         */
        var ids = [];
        var orders = [];
        $('td.index', ui.item.parent()).each(function (i) {
            $(this).html(i + 1);
            ids.push($(this).data('id'));// gets the id of the row.
            orders.push(i + 1); // gets the display order of the row.
        });


        $.ajax({
            type: "POST",
            url: $(this).data('order-url'),
            data: { clubId : $('#clubId').val(), idList: ids, orderList : orders },
            cache: false
        });
    };

    $("#pic-table tbody").sortable({//uses jquery sortable plugin for dragging and dropping table rows. Used for changing display order of the images.
        helper: fixHelperModified,
        stop: updateIndex
    }).disableSelection();

    $('.delete').on('click', function (pos, item) {
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
        /**
         * Is called for toggling the activation of the image.
         */
        var pictureId = $(this).data('id');
        var current = $(this);
        $.ajax({
            type: "POST",
            url: $(this).data('activation-url'),
            data: { id: pictureId },
            cache: false,
            success: function (data) {
                if (data === 'true') {
                    current.removeClass('glyphicon-remove');
                    current.addClass('glyphicon-ok');
                    current.attr('title', 'activated');
                } else {
                    current.removeClass('glyphicon-ok');
                    current.addClass('glyphicon-remove');
                    current.attr('title', 'deactivated');
                }
               
            }
        });
    });

    $('.default').on('click', function (pos, item) {
        /**
         * /Is called for setting an image as the cover photo.
         */
         var clubId = $('#clubId').val();
        var pictureId = $(this).data('id');
        var current = $(this);
        $.ajax({
            type: "POST",
            url: $(this).data('default-url'),
            data: { clubId : clubId, picId: pictureId },
            cache: false,
            success: function (data) {
                $('.glyphicon-star').each(function (index, item) {
                    $(item).removeClass('glyphicon-star');
                    $(item).addClass('glyphicon-star-empty');
                    $(item).attr('title', 'make this cover photo');
                });
                current.removeClass('glyphicon-star-empty');
                current.addClass('glyphicon-star');
                current.attr('title', 'cover photo');
            }
        });
    });
});