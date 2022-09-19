
import styled from 'styled-components';
import {pointer} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  ${pointer};
  display: flex;
  height: 30px;
  justify-content: center;;
  width: 30px;
`;

export const Icon = styled.img.attrs({
  src: `${PUBLIC_URL}/icons/left-arrow.svg`,
})`
  align-self: center;
  height: 12px;
`;
