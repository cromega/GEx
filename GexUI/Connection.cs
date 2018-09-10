﻿using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System;
using System.ComponentModel;

namespace GexUI {
    public class Connection :INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public AudioNode Source;
        public AudioNode Target;

        private bool _IsSelected;
        public bool IsSelected {
            get { return _IsSelected; }
        }

        public double X1 { get { return _X1(); } }
        public double Y1 { get { return _Y1(); } }
        public double X2 { get { return _X2(); } }
        public double Y2 { get { return _Y2(); } }

        public Connection(AudioNode source, AudioNode target) {
            Source = source;
            Target = target;

            _IsSelected = false;
        }

        public void Update() {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X1"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y1"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X2"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y2"));
        }

        public bool IsAttachedTo(AudioNode node) {
            return Source == node || Target == node;
        }

        public bool NearTo(Point point) {
            var distance = Math.Abs((X2 - X1) * (Y1 - point.Y) - (X1 - point.X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
            return distance < 10;
        }

        public void Select() {
            _IsSelected = true;
        }

        public void Deselect() {
            _IsSelected = false;
        }

        private double _X1() {
            return Canvas.GetLeft(Source) + Target.ActualWidth;
        }

        private double _Y1() {
            return Canvas.GetTop(Source) + Source.ActualHeight / 2;
        }

        private double _X2() {
            return Canvas.GetLeft(Target);
        }


        private double _Y2() {
            return Canvas.GetTop(Target) + Target.ActualHeight / 2;
        }
    }
}
