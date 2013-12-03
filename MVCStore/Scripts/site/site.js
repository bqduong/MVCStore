// Run our startup routines when the DOM is ready.

var Site = (function ($, Backbone, Handlebars) {
    var initializeApp = function(baseUrl) {
        var navigationPanelView = new NavigationPanelView({ el: ".featuredModel" });
        navigationPanelView.render();
    };

    return {
        init: function(baseUrl) {
            initializeApp(baseUrl);
        },
      
        getTemplate: function (name) {
            var template = "";
            if (Handlebars.templates) {
                var temp = Handlebars.templates[name + "-template"];
                if (!temp) {
                    console.error("Template not found: " + name);
                }
                template = temp;
            }
            return template;
        }
};
})(jQuery, Backbone, Handlebars);

$(document).ready(function() {
    Site.init($("body").data("baseUrl"));
});