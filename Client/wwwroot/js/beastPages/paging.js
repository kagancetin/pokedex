export var myPagination;
export var a;
export var b;

export function reducePaginationList(page) {
    var pageItemList = document.querySelectorAll(".page-item-pages");
    console.log(pageItemList.length)
    if (pageItemList.length > 9) {
        if (a == undefined) {
            a = document.createElement("li");
            a.className = "pagiDots";
            a.textContent = "...";
            b = document.createElement("li");
            b.className = "pagiDots";
            b.textContent = "...";
            pageItemList[0].parentElement.appendChild(a);
            pageItemList[0].parentElement.appendChild(b);
            pageItemList[0].parentElement.insertBefore(a, pageItemList[1]);
            pageItemList[0].parentElement.insertBefore(b, pageItemList[pageItemList.length - 1]);
            a.style.width = pageItemList[pageItemList.length - 1].style.width
            b.style.width = pageItemList[pageItemList.length - 1].style.width
        }
        for (var i = 0; i < pageItemList.length; i++) {
            if (page < 3) {
                a.style.display = "none"
                b.style.display = "block"
                if (i > 6)
                    pageItemList[i].style.display = "none"
                else
                    pageItemList[i].style.display = "block"
            }

            else if (page >= pageItemList.length - 3) {
                a.style.display = "block"
                b.style.display = "none"

                if (i < pageItemList.length - 7)
                    pageItemList[i].style.display = "none"
                else
                    pageItemList[i].style.display = "block"
            }
            else {
                a.style.display = "block"
                b.style.display = "block"

                if (i > page + 1 || i < page - 3)
                    pageItemList[i].style.display = "none"
                else
                    pageItemList[i].style.display = "block"

            }
            if (i == 0 || i == pageItemList.length - 1) {
                pageItemList[i].style.display = "block"
            }
        }
    }
    else {
        if (a != undefined) {
            var pagiDots = document.querySelectorAll(".pagiDots")
            pagiDots.forEach(x => x.parentElement.removeChild(x));
        }
        a = undefined;
        b = undefined;
    }
}
