
import styled from 'styled-components';
import {Radio} from '~/components';
import {PageSubtitle} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

export const DPPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const RadioInput = styled(Radio)`
  margin-bottom: 15px;
`;
