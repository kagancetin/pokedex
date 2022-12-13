var onHoverTypeItem = (e) => {
    var color = e.style.backgroundColor;
    var backgroundColor = e.style.color;
    e.style.backgroundColor = backgroundColor;
    e.style.color = color;
    e.onmouseleave = () => {
        e.style.backgroundColor = color;
        e.style.color = backgroundColor;
    }
}
