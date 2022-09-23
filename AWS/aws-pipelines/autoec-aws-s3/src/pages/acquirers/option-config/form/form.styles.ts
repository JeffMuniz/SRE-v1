
import styled, {css} from 'styled-components';
import {Input, LoadingButton} from '~/components';
import {
 Clickable, Spacer, mqTablet,
} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const AffiliationNumberInput = styled(Input).attrs({
  placeholder: '0000000000',
})`
  label {
    display: none;
  }
  &:first-child label {
    display: block;
  }
`;

export const AddLineLabel = styled(Clickable)`
  display: flex;
  line-height: 20px;
  margin-top: 10px;
`;

export const CirclePlusIcon = styled.img.attrs({
  src: `${PUBLIC_URL}/icons/circle-plus.svg`,
})`
  height: 20px;
  margin-right: 5px;
`;

export const FSpacer = styled(Spacer)``;

export const ContinueButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;
