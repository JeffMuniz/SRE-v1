import styled, {css} from 'styled-components';
import {Modal} from '~/components';
import {BaseLabel, PrimaryButton} from '~/styled';

export const Wrapper = styled(Modal)``;

export const Title = styled(BaseLabel)`
  font-size: 28px;
  font-weight: 700;
  line-height: 36px;
  ${({theme: {colors}}) => css`
  color: ${colors.text.title};
  `}
`;

export const Text = styled(BaseLabel)`
  font-size: 16px;
  font-weight: 400;
  line-height: 24px;
  margin-top: 12px;
  ${({theme: {colors}}) => css`
    color: ${colors.text.common};
  `}
`;

export const Button = styled(PrimaryButton)`
  align-self: center;
  justify-content: center;
  margin-top: 50px;
`;

export const ButtonText = styled(BaseLabel)`
  align-self: center;
  font-size: 14px;
  font-weight: bold;
  transition: ease .3s;
  ${({theme: {colors}}) => css`
    color: ${colors.palette.white.main};
  `}
`;
