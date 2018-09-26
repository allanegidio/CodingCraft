var LayoutController = function (cookieService) {

    var init = function (container) {
        setCurrentLayout(container);

        $(container).on("change", layoutHandler);
    }

    var layoutHandler = function (e) {
        var layouts = e.target;
        var currentLayout = layouts.selectedIndex;
        var layoutName = layouts.options[currentLayout].text;

        cookieService.setCookie("LayoutName", layoutName);
        cookieService.setCookie("CurrentLayout", currentLayout);

        window.location.reload();
    }
    
    var setCurrentLayout = function (container) {
        $(container).val(cookieService.getCookie('CurrentLayout'));
    }

    return {
        init: init
    }

}(CookieService);