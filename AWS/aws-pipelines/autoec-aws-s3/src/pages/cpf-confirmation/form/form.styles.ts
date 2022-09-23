
import styled, {css} from 'styled-components';
import {Input} from '~/components';
import {
 PrimaryButton, Spacer, mqTablet,
} from '~/styled';

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

export const NameInput = styled(Input)`
  margin-bottom: 20px;
`;

export const CPFInput = styled(NameInput)`
  margin: 0;
`;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(PrimaryButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;
