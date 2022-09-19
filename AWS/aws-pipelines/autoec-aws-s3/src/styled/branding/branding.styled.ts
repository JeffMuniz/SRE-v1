
import styled from 'styled-components';

const {PUBLIC_URL} = process.env;

export const macLogo = styled.img.attrs({
  src: `${PUBLIC_URL}/images/mac-logo.png`,
})`
  display: block;
  height: 512px;
  width: 512px;
`;

export const macLogo2 = styled(macLogo).attrs({
  src: `${PUBLIC_URL}/images/mac-logo-2.png`,
})``;
