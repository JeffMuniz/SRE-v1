
import styled, {css} from 'styled-components';
import {Input, LoadingButton} from '~/components';
import {Spacer, mqTablet} from '~/styled';

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

export const BankInput = styled(Input).attrs({
  id: 'bank',
  label: 'Banco:',
  name: 'bank',
  placeholder: 'Nome do banco',
})`
  margin-bottom: 20px;
`;

export const TabletInlineWrapper = styled.div`
  ${mqTablet(css`
    display: flex;
  `)}
`;

export const AgencyInput = styled(BankInput).attrs<Input.Props>({
  id: 'agency',
  label: 'Agência:',
  name: 'agency',
  placeholder: '0000',
})`
  ${mqTablet(css`
    width: 200px;
  `)}
`;

export const InlineWrapper = styled.div`
  display: flex;
  margin-bottom: 30px;
  width: 100%;
`;

export const AccountInput = styled(BankInput).attrs<Input.Props>({
  id: 'account',
  label: 'Conta corrente jurídica:',
  name: 'account',
  placeholder: '00000',
})`
  align-self: flex-start;
  margin-bottom: 0;
  flex-grow: 1;
`;

export const DigitInput = styled(AccountInput).attrs<Input.Props>({
  id: 'digit',
  label: 'Dígito:',
  name: 'digit',
  placeholder: '0',
})`
  max-width: 100px;
`;

export const FSpacer = styled(Spacer)``;

export const ContinueButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;
