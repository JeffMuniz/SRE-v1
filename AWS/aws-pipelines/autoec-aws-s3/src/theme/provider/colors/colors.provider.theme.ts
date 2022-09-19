
import {darken, lighten} from 'polished';

export default {
  feedback: {
    info: '#409cf7',
    success: '#2ad178',
    warning: '#f9d652',
    failure: '#cc3030',
    neutral: '#EBEBEB',
  },
  text: {
    title: '#666666',
    common: '#979797',
    lighter: '#dadada',
  },
  palette: (() => {
    const black = '#333333';
    const blue = '#409cf7';
    const green = '#2ad178';
    const lightGray = '#dadada';
    const pink = '#ff053f';
    const white = '#ffffff';

    return {
      blue: {
        main: blue,
        contrast: '#ffffff',
      },
      green: {
        main: green,
        contrast: '#ffffff',
      },
      white: {
        main: white,
      },
      pink: {
        main: pink,
        dark: darken(0.1, pink),
        light: lighten(0.2, pink),
        contrast: '#ffffff',
      },
      lightGray: {
        main: lightGray,
        light: lighten(0.105, lightGray), // #f5f5f5
        contrast: '#333333',
      },
      black: {
        main: black,
        contrast: '#ffffff',
      },
    };
  })(),
};
