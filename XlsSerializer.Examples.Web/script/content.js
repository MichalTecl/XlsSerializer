var app = app || {};
app.content = app.content || {};
app.content.load = app.content.load || function(data) {

    app.sourcesList.initialize(data.Sources);

}; 