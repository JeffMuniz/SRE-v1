
import styled, {css} from 'styled-components';
import {Input} from '~/components';
import {mqTablet} from '~/styled';

export const Wrapper = styled.div`
  ${mqTablet(css`
    display: flex;
  `)}
`;

export const BankInput = styled(Input).attrs({
  label: 'Banco:',
  showEditIndicator: true,
})`
  margin-bottom: 20px;

  ${mqTablet(css`
    flex-grow: 1;
    input {
      padding-right: 70px;
    }
    img {
      margin-left: -60px;
    }
  `)}
`;

export const AgencyInput = styled(BankInput).attrs<Input.Props>({
  label: 'Agência:',
})`
  ${mqTablet(css`
    max-width: 150px;
  `)}
`;

export const AccountInput = styled(BankInput).attrs<Input.Props>({
  label: 'Conta corrente jurídica:',
})``;

export const DigitInput = styled(Input).attrs({
  label: 'Dígito:',
  showEditIndicator: true,
})`
  ${mqTablet(css`
    max-width: 100px;
  `)}
`;
