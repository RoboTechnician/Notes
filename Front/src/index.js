import React from "react";
import { render } from 'react-dom';
import Body from './components/body';

let pages = { register: '/register', login: '/login', index: '/' };
let page = window.location.pathname;

render(
    <Body page={page} pages={pages} />,
    document.querySelector("#root")
);