var lazyLoad = (isEnd, funcName, nav, dotnetInstance) => {
    if (window.location.pathname != nav) {
        document.onscroll = undefined;
    }
    else {
        var waitAminute = false
        document.onscroll = (e) => {
            if (window.location.pathname != nav) {
                document.onscroll = undefined;
            }
            else {
                var h = document.documentElement,
                    b = document.body,
                    st = 'scrollTop',
                    sh = 'scrollHeight';
                var percent = (h[st] || b[st]) / ((h[sh] || b[sh]) - h.clientHeight) * 100;
                if (percent > 80 && !waitAminute && !isEnd) {
                    waitAminute = true;
                    dotnetInstance.invokeMethodAsync(funcName);
                    setTimeout(() => { waitAminute = false }, 3000);
                }
            }

        }
    }
}