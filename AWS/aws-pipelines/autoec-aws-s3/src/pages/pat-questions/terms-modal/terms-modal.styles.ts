
import styled, {css} from 'styled-components';
import {
 Checkbox, LoadingButton, Modal,
} from '~/components';
import {PageSubtitle, mqTablet} from '~/styled';

export const Wrapper = styled(Modal)``;

export const TMPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 30px;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
`;

export const CheckboxesWrapper = styled.div`
  margin-bottom: 30px;
`;

export const TMCheckbox = styled(Checkbox)`
  margin-bottom: 20px;
  &:last-child {
    margin-bottom: 0;
  }
  & > div {
    margin-right: 10px;
  }
  label {
    font-size: 14px;
  }
`;

export const ContinueButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: center;
  `)}
`;
