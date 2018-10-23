var LayoutController = function (cookieService) {

    var init = function (container) {
        setCurrentLayout(container);

        $(container).on("change", layoutHandler);
    }

    var layoutHandler = function (e) {
        var layouts = e.target;
        var currentLayout = e.target.selectedIndex;
        var layoutName = layouts.options[currentLayout].text;

        cookieService.setCookie("LayoutName", layoutName);

        window.location.reload();
    }
    
    var setCurrentLayout = function (container) {
        $("#LayoutName option").each(function () {
            if ($(this).text() == cookieService.getCookie('LayoutName')) {
                $(this).prop('selected', 'true'); 
            }
        });
    }

    return {
        init: init
    }

}(CookieService);