
import {createGlobalStyle} from 'styled-components';
import fonts from './fonts/fonts.global.theme';

export default createGlobalStyle`
  ${fonts}
  
  *, *:before, *:after {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
  }

  body {
    overflow: auto;
    font-family: sans-serif;
  }

  h1, h2, h3, h4, h5, h6, label, p, input, button {
    font-family: "Open Sans", sans-serif;
    letter-spacing: 0.5px;;
  }

  button, input {
    &:active, &:focus, &:focus-within, &:hover, &:visited {
      outline: none;
    }
  }

  input {
    &[type=number] {
      -moz-appearance: textfield;
    }
    &::-webkit-inner-spin-button, &::-webkit-outer-spin-button {
      -webkit-appearance: none;
      margin: 0;
    }
  }
  
  img {
    display: block;
  }
`;
