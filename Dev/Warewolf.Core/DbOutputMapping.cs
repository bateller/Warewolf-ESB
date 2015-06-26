﻿using System;
using Dev2.Common.Interfaces.DB;
using Microsoft.Practices.Prism.Mvvm;

namespace Warewolf.Core
{
    public class ServiceOutputMapping : BindableBase,IServiceOutputMapping, IEquatable<ServiceOutputMapping>
    {
        string _outputName;
        string _recordSetName;

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ServiceOutputMapping other)
        {
            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
            {
                return false;
            }
            if(ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((ServiceOutputMapping)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return 397 ^ Name.GetHashCode() ^ OutputName.GetHashCode();
        }

        public static bool operator ==(ServiceOutputMapping left, ServiceOutputMapping right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ServiceOutputMapping left, ServiceOutputMapping right)
        {
            return !Equals(left, right);
        }

        #endregion

        public ServiceOutputMapping(string name, string mapping)
        {
            Name = name;
            OutputName = mapping;
        }

        #region Implementation of IDbOutputMapping

        public string Name { get; set; }
        public string OutputName
        {
            get
            {
                return _outputName;
            }
            set
            {
                _outputName = value;
                OnPropertyChanged(() => OutputName);
                OnPropertyChanged(() => CompleteName);
            }
        }
        public string RecordSetName
        {
            get
            {
                return _recordSetName;
            }
            set
            {
                _recordSetName = value;
                OnPropertyChanged(() => RecordSetName);
                OnPropertyChanged(() => CompleteName);
            }
        }
        public string CompleteName
        {
            get
            {
                return RecordSetName + "." + OutputName;
            }
        }

        #endregion
    }
}