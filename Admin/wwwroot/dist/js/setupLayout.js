$('[data-widget="treeview"]').Treeview('init')

restartNavbarButton()

function restartNavbarButton() {
    $('li.nav-item a').filter(function () {
        if (this.getAttribute("href") != window.location.pathname)
            $(this).removeClass('active')
        $(this).parentsUntil(".nav-sidebar > .nav-treeview").prev('a')
            .removeClass('active');
        return this.getAttribute("href") == window.location.pathname;
    }).addClass('active').parentsUntil(".nav-sidebar > .nav-treeview")
        .css({ 'display': 'block' })
        .addClass('menu-open').prev('a')
        .addClass('active');
}

