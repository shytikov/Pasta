$(document).ready(function () {
    function pasteHandler(jQueryEvent) {
        /// <summary>Handles Ctrl+V event.</summary>
    }

    $("div.main form input:submit").click(function (event) {
        var content = $("div.main form #Content").html();

        if (content == "") {
            return false;
        }

        event.preventDefault();

        var id;

        $.post(
            "/Create",
            { "Content": content },
            function (pastie) {
                id = pastie.Id;
                $("div.main h1").text("#" + pastie.Id + "");
            },
            "json"
        )

        var main = $("div.main");

        main.slideUp(function () {
            $("div.main form").remove();
            if (undefined == id) {
                main.prepend("<h1>#</h1>");
            } else {
                main.prepend("<h1>#" + id + "</h1>");
            }

            main.append("<pre><code class='prettyprint linenums'>" + $('<div/>').text(content).html() + "</code></pre>");
            main.slideDown();

            $.getScript(
                "/content/js/vendor/prettify.js",
                function () {
                    prettyPrint();
                });
        });
    })
});	
