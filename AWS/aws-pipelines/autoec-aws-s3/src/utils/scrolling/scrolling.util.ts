
import {isVisible as elementIsVisible} from 'element-is-visible';
import SweetScroll from 'sweet-scroll';
import {ScrollableElement} from 'sweet-scroll/decls/types';

export const toElement = ({
  autoDestroy = true,
  container = window,
  element,
  duration = 600,
}: {
  autoDestroy?: boolean;
  container?: ScrollableElement;
  duration?: number;
  element: HTMLElement;
}): SweetScroll => {
  const sweetScroll = new SweetScroll({duration}, container);
  sweetScroll.toElement(element);
  if(autoDestroy) {
    setTimeout(sweetScroll.destroy, duration);
  }
  return sweetScroll;
};

export const isVisible = ({element}: { element: Element }) => {
  return elementIsVisible(element as HTMLElement);
};
