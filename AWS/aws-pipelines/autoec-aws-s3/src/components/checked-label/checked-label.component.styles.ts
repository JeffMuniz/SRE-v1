
import styled, {css} from 'styled-components';
import {BaseLabel, SVGImporter} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  display: flex;
`;

export const Icon = styled(SVGImporter).attrs({
  src: `${PUBLIC_URL}/icons/check-mark.svg`,
})`
  align-self: center;
  margin-right: 5px;
  height: 12px;
  width: 16px;
  ${({theme: {colors}}) => css`
    path {
      fill: ${colors.palette.green.main};
    }
  `}
`;

export const Label = styled(BaseLabel)`
  align-self: center;
  font-size: 16px;
  ${({theme: {colors}}) => css`
    color: ${colors.palette.green.main};
  `}
`;

