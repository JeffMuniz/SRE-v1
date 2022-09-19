
import styled, {css} from 'styled-components';

export const BaseInput = styled.input.attrs({
  autoComplete: 'off',
})`
  border: none;
  font-size: 26px;
  font-weight: 300;
  height: 45px;
  padding: 0 3px;
  width: 100%;

  ${({theme: {colors}}) => css`
    background-color: ${colors.palette.white.main};
    border-bottom: 1px solid ${colors.text.common};
    color: ${colors.palette.pink.main};

    &::placeholder {
      color: ${colors.text.lighter};
    }
  `}
`;
