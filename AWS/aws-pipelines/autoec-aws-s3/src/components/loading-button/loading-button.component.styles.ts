
import {Spinner2} from '@styled-icons/icomoon/Spinner2';
import styled, {css} from 'styled-components';

import {BaseLabel, PrimaryButton} from '~/styled';
import {spin} from '~/styled';

export const Wrapper = styled(PrimaryButton)`
  display: flex;
  justify-content: center;
`;

export const Text = styled(BaseLabel).attrs({
  as: 'span',
})<{ extraMargin: boolean; }>`
  align-self: center;
  font-weight: bold;
  transition: ease .3s;

  ${({theme: {colors}}) => css`
    color: ${colors.palette.pink.contrast};
  `}

  ${({extraMargin}) => extraMargin && css`
    margin-right: 10px;
  `}
`;

export const Loading = styled(Spinner2)<{ show: boolean; }>`
  ${spin({
    animationName: 'loading-button-spin',
  })}
  align-self: center;
  height: 18px;
  opacity: 0;
  transform: scale(0);
  transition: ease .3s;
  width: 0;

  ${({show}) => show && css`
    opacity: 1;
    transform: scale(1);
    width: 18px;
  `}
`;
