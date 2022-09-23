
import {FC} from 'react';
import {ThemeProvider} from 'styled-components';

import {default as colors} from './colors/colors.provider.theme';

const Provider: FC = ({children}) => (
  <ThemeProvider theme={{colors}}>
    {children}
  </ThemeProvider>
);

export default Provider;
