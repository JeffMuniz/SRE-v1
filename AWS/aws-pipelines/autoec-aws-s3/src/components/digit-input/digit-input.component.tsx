
import {
  FC,
  KeyboardEvent,
  memo,
  useCallback,
  useEffect,
  useRef,
  useState,
} from 'react';
import {isEqual} from '~/utils';
import {
 Input, InputWrapper, Label, Wrapper,
} from './digit-input.component.styles';

const DigitInput: FC<DigitInput.Props> = ({
  autoFocus = false,
  className,
  digits,
  label,
  onChange,
}) => {

  const firstInputRef = useRef<HTMLInputElement>(null);

  const [ state, setState ] = useState<DigitInput.State>({
    raw: '',
    splitted: new Array(digits).fill(''),
  });

  useEffect(() => {
    setTimeout(() => {
      if(!autoFocus || !firstInputRef.current) return;
      firstInputRef.current.focus();
    }, 1000);
  }, [ autoFocus, firstInputRef ]);

  const handleKeyUp = useCallback(({event, index}: {
    event: KeyboardEvent<HTMLInputElement>;
    index: number;
  }) => {
    const target = event.target as HTMLInputElement;
    const previousSibling = target.previousSibling as HTMLInputElement;
    const nextSibling = target.nextSibling as HTMLInputElement;

    const splitted = [ ...state.splitted ];
    const setSplitted = () => {
      const raw = splitted.join('');
      setState(prev => ({...prev, splitted, raw}));
      onChange(raw);
    };

    const {code} = event;
    if(code === 'ArrowLeft') {
      previousSibling?.focus();
      previousSibling?.select();
    } else if(code === 'ArrowRight') {
      nextSibling?.focus();
      nextSibling?.select();
    } else if (code === 'Backspace') {
      splitted[index] = '';
      setSplitted(); // Here for a faster visual response.
      previousSibling?.focus();
      previousSibling?.select();
    } else if(code.includes('Digit') || code.includes('Numpad')) {
      splitted[index] = code.replace('Digit', '')
        .replace('Numpad', '');
      setSplitted(); // Here for a faster visual response.
      nextSibling?.focus();
    }
  }, [
    state.splitted,
    onChange,
  ]);

  return (
    <Wrapper className={className}>
      {label && <Label>{label}</Label>}
      <InputWrapper>
        {state.splitted.map((v, index) => (
          <Input
            ref={ref => {
              if(autoFocus && index === 0) firstInputRef.current = ref;
            }}
            key={index}
            value={v}
            onChange={() => { }}
            onFocus={event => event.target.select()}
            onKeyUp={event => handleKeyUp({event, index})}
          />
        ))}
      </InputWrapper>
    </Wrapper>
  );
};

export default memo(DigitInput, isEqual);
