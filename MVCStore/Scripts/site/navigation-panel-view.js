var NavigationPanelView = Backbone.View.extend({

    initialize: function () {
        _.bindAll(this, "render", "showReports", "showDashboard");
        this.template = Site.getTemplate("navigation-bar");
        //this.model.on("change mode", this.render);
    },
    //template: Handlebars.templates["navigation-bar-template"],
    
    //events: {
    //    "click .dashboard-select": "showDashboard",
    //    "click .report-select": "showReports"
    //},

    render: function () {
        this.$el.html(this.template());
        //switch (this.model.get("mode")) {
        //    case "reports":
        //        this.showReports();
        //        break;
        //    case "dashboards":
        //        this.showDashboard();
        //        break;
        //}
    },

    showReports: function (e) {
        //if (e) e.preventDefault();
        //this.$el.find("li").removeClass("active");
        //this.$el.find(".report-select").toggleClass("active");
        //this.model.set("mode", "reports");
    },

    showDashboard: function (e) {
        //if (e) e.preventDefault();
        //this.$el.find("li").removeClass("active");
        //this.$el.find(".dashboard-select").toggleClass("active");
        //this.model.set("mode", "dashboards");
    }

});