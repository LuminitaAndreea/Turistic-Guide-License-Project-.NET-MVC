<script type="text/javascript">
    function CRateOut(rating) {
        for (var i = 1; i <= rating; i++) {
        $("#span" + i).attr('class', 'glyphicon glyphicon-star-empty');
        }

    }
    function CRateOver(rating) {
        for (var i = 1; i <= rating; i++) {
        $("#span" + i).attr('class', 'glyphicon glyphicon-star');
        }

    }
    function CRateClick(rating) {
        $("#lblRating").val(rating);
        for (var i = 1; i <= 5; i++) {
        $("#span" + i).attr('class', 'glyphicon glyphicon-star');
        }

        for (var i = rating + 1; i <= rating; i++) {
        $("#span" + i).attr('class', 'glyphicon glyphicon-star-empty');
        }


    }
    function CRateSelected(rating) {
        var rating = $("#lblRating").val();
        for (var i = 1; i <= rating; i++) {
        $("#span" + i).attr('class', 'glyphicon glyphicon-star');
        }


    }
    function VerifyRating() {
        var rating = $("#lblRating").val();
        if (rating == "0") {
        alert("Please select rating");
            return false;
        }
    }
</script>
