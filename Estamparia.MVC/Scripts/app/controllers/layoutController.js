var LayoutController = function () {

    var init = function (container) {
        setCurrentLayout(container);

        $(container).on("change", layoutHandler);
    }

    var layoutHandler = function (e) {
        var layouts = e.target;
        var currentLayout = layouts.selectedIndex;
        var layoutName = layouts.options[currentLayout].text;

        localStorage.setItem('currentLayout', currentLayout);

        document.cookie = `LayoutName = ${layoutName}`;

        window.location.reload();
    }
    
    var setCurrentLayout = function (container) {
        if (localStorage.getItem('currentLayout'))
            $(container).val(localStorage.currentLayout);
    }

    return {
        init: init
    }

}();